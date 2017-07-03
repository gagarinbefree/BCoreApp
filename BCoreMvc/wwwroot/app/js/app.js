/// <reference path="../../typings/jquery/jquery.d.ts" />
/// <reference path="../../typings/bootstrap/bootstrap.d.ts" />
/// <reference path="../../typings/highlightjs/highlightjs.d.ts" />
/// <reference path="../../typings/ace/index.d.ts" />
var Blog;
(function (Blog) {
    var Code = (function () {
        function Code(owner) {
            this.elModalForm = $('#codeModalForm');
            this.elButtonUpload = $("#codeButtonUpload");
            this.owner = owner;
            this.init();
        }
        Code.prototype.init = function () {
            var _this = this;
            this.elModalForm.on('shown.bs.modal', function (e) { return _this.formShow(e); });
            this.elButtonUpload.on('click', function (e) { return _this.submit(e); });
        };
        Code.prototype.formShow = function (e) {
            this.initAceEditor();
            this.elButtonUploadDisabled();
        };
        Code.prototype.initAceEditor = function () {
            var _this = this;
            this.editor = ace.edit('codeText');
            this.editor.setTheme('ace/theme/twilight');
            this.editor.renderer.setShowGutter(false);
            this.editor.focus();
            this.editor.on('change', function (e) { return _this.editorChangeValue(e); });
        };
        Code.prototype.elButtonUploadDisabled = function () {
            this.elButtonUpload.prop('disabled', typeof (this.editor) == 'undefined' || this.isEmpty(this.editor.getValue()));
        };
        Code.prototype.editorChangeValue = function (e) {
            this.elButtonUploadDisabled();
        };
        Code.prototype.isEmpty = function (str) {
            return str.replace(new RegExp('\r\n', 'g'), '').replace(/\s/g, '').trim().length == 0;
        };
        Code.prototype.submit = function (e) {
            var _this = this;
            this.owner.elCode.val(this.editor.getValue());
            this.owner.post(e);
            setTimeout(function () {
                _this.elModalForm.modal('hide');
            }, 250);
        };
        return Code;
    }());
    Blog.Code = Code;
})(Blog || (Blog = {}));
/// <reference path="../../typings/jquery/jquery.d.ts" />
/// <reference path="../../typings/jquery.fileupload/jquery.fileupload.d.ts" />
/// <reference path="../../typings/nprogress/nprogress.d.ts" />
/// <reference path="../../typings/bootstrap/bootstrap.d.ts" />
/// <reference path="../../typings/imagefill.js/imagefill.js.d.ts" />
var Blog;
(function (Blog) {
    var Image = (function () {
        function Image(owner) {
            this.elBrowseText = $("#imageBrowseText");
            this.elPreview = $("#imageUploadPreview");
            this.elBrowseButton = $("#imageBrowseBtnUpload");
            this.elInput = $("#imageInputUpload");
            this.elButtonUpload = $("#imageButtonUpload");
            this.elFileName = $("#imageFileNameUpload");
            this.elModalForm = $("#imageModalForm");
            this.reader = new FileReader();
            this.dataSubmit = null;
            this.owner = owner;
            this.url = "/FileHandler/Upload?objectContext=" + this.owner.userId;
            this.init();
        }
        Image.prototype.init = function () {
            var _this = this;
            this.elButtonUpload.prop("disabled", true);
            NProgress.configure({
                parent: "#imageProgressUpload",
                showSpinner: false
            });
            this.elInput.fileupload({
                url: this.url,
                dataType: 'json',
                autoUpload: false,
                singleFileUploads: true,
                acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i,
                add: function (e, data) { return _this.addDataSubmit(e, data); },
                start: function () { return _this.startUpload(); },
                done: function (e, data) { return _this.doneUpload(e, data); }
            });
            this.elBrowseButton.on('click', function (e) { return _this.browse(e); });
            this.elButtonUpload.on('click', function (e) { return _this.submit(e); });
            this.elInput.on('change', function (e) { return _this.inputChange(e); });
            this.reader.onload = function (e) { return _this.onLoad(e); };
        };
        Image.prototype.browse = function (e) {
            this.dataSubmit = null;
            this.elInput.html(this.elInput.html());
        };
        Image.prototype.inputChange = function (e) {
            var el = e.currentTarget;
            var path = el.value;
            this.elFileName.val(path.replace(/^.*[\\\/]/, ''));
            this.reader.readAsDataURL(el.files[0]);
        };
        Image.prototype.submit = function (e) {
            if (this.dataSubmit != null)
                this.dataSubmit.submit();
        };
        Image.prototype.onLoad = function (e) {
            this.elPreview.attr("src", this.reader.result);
            this.elButtonUpload.prop("disabled", false);
            this.elBrowseText.hide();
        };
        Image.prototype.startUpload = function () {
            NProgress.start();
        };
        Image.prototype.addDataSubmit = function (e, data) {
            this.dataSubmit = data;
        };
        Image.prototype.doneUpload = function (e, data) {
            var _this = this;
            NProgress.done(true);
            this.owner.elImageUrl.val(data.result.files.length > 0 ? data.result.files[0].url : "");
            this.owner.post(e);
            setTimeout(function () {
                _this.elModalForm.modal("hide");
            }, 250);
        };
        return Image;
    }());
    Blog.Image = Image;
})(Blog || (Blog = {}));
/// <reference path="../../typings/jquery/jquery.d.ts" />
/// <reference path="../../typings/autosize/autosize.d.ts" />
/// <reference path="../../typings/highlightjs/highlightjs.d.ts" />
/// <reference path="../../typings/simplemde/simplemde.d.ts" />
// <reference path="../../ts/common.ts" />
var Blog;
(function (Blog) {
    var WhatsNew = (function () {
        function WhatsNew(userId) {
            var _this = this;
            this.elSubmitForm = $("#whatsNewForm");
            this.elInput = $("#whatsNewText");
            this.elImageUrl = $("#whatsNewImageUrl");
            this.elVideoUrl = $("#whatsNewVideoUrl");
            this.elGeo = $("#whatsNewGeo");
            this.elCode = $("#whatsCode");
            this.elPostButton = $("#whatsNewPostButton");
            this.elPost = $("#whatsNewPost");
            this.elDropdown = $(".dropdown-toggle");
            this.userId = userId;
            this.elPostButton.on("click", function (e) { return _this.post(e); });
            this.init();
        }
        WhatsNew.prototype.init = function () {
            this.modalImage = new Blog.Image(this);
            this.modalCode = new Blog.Code(this);
            autosize(this.elInput);
            this.elInput.val("");
            autosize.update(this.elInput);
            this.elInput.focus();
            this.elImageUrl.val("");
            this.elDropdown.dropdown();
            //Common.App.ace();
        };
        WhatsNew.prototype.post = function (e) {
            var _this = this;
            e.preventDefault();
            this.elPost.load("/Update/Post", this.elSubmitForm.serializeArray(), function () { return _this.init(); });
        };
        return WhatsNew;
    }());
    Blog.WhatsNew = WhatsNew;
})(Blog || (Blog = {}));
/// <reference path="../../typings/jquery/jquery.d.ts" />
/// <reference path="../../typings/autosize/autosize.d.ts" />
/// <reference path="../../typings/jquery-appear/jquery.appear.js.d.ts" />
var Post;
(function (Post) {
    var Comment = (function () {
        //private isFocus: boolean = true;
        function Comment() {
            var _this = this;
            this.elBody = $("body");
            this.elInput = $("#commentInput");
            this.elDropdown = $(".dropdown-toggle");
            autosize(this.elInput);
            autosize.update(this.elInput);
            this.elDropdown.dropdown();
            this.elInput.val("");
            this.elInput.appear();
            this.elInput.on("appear", function (e, elements) { return _this.inputAppear(e, elements); });
        }
        Comment.prototype.inputAppear = function (e, elements) {
            //if (location.hash == "#commentAnchor" && this.isFocus) {
            if (location.hash == "#commentAnchor") {
                elements.focus();
                //this.isFocus = false;
            }
        };
        return Comment;
    }());
    Post.Comment = Comment;
})(Post || (Post = {}));
/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/bootbox/index.d.ts" />
var Common;
(function (Common) {
    var App = (function () {
        function App() {
            this.confirm = new ConfirmDialog();
            //App.ace();
        }
        return App;
    }());
    Common.App = App;
    var Pager = (function () {
        function Pager(url, page, elContainer, elButtonDone) {
            var _this = this;
            this.url = url;
            this.page = Number(page);
            this.elContainer = elContainer;
            this.elButtonDone = elButtonDone;
            if (this.elButtonDone)
                this.elButtonDone.on('click', function (e) { return _this.loadPage(e); });
        }
        Pager.prototype.loadPage = function (e) {
            var _this = this;
            if (this.elContainer) {
                this.elContainer.append($('<div>').load(this.url, { page: this.page + 1 }, function (responseText, textStatus, XMLHttpRequest) { return _this.init(responseText, textStatus, XMLHttpRequest); }));
            }
        };
        Pager.prototype.init = function (responseText, textStatus, XMLHttpRequest) {
            if (textStatus != 'success'
                || XMLHttpRequest.status != 200
                || this.isEmpty(responseText)
                || !this.isHtml(responseText)) {
                this.elButtonDone.hide();
            }
            else
                this.page++;
        };
        Pager.prototype.isEmpty = function (str) {
            return str.replace(new RegExp('\r\n', 'g'), '').replace(/\s/g, '').trim().length == 0;
        };
        Pager.prototype.isHtml = function (str) {
            return /<[a-z\][\s\S]*>/i.test(str);
        };
        return Pager;
    }());
    Common.Pager = Pager;
    var ConfirmDialog = (function () {
        function ConfirmDialog() {
            var _this = this;
            this.elConfirm = $('a[data-confirm]');
            this.elConfirm.on('click', function (e) { return _this.showDialog(e); });
        }
        ConfirmDialog.prototype.showDialog = function (e) {
            var _this = this;
            e.preventDefault();
            this.elTarget = $(e.target);
            bootbox.confirm({
                title: this.elTarget.attr('data-confirm-title'),
                message: this.elTarget.attr('data-confirm'),
                size: 'small',
                buttons: {
                    confirm: {
                        label: 'Yes',
                        className: 'btn-danger'
                    },
                    cancel: {
                        label: 'No',
                        className: 'btn-success'
                    }
                },
                callback: function (result) { return _this.handlerDialogResult(result); }
            });
        };
        ConfirmDialog.prototype.handlerDialogResult = function (result) {
            if (result) {
                var href = this.elTarget.attr('href');
                if (typeof (href) !== "undefined" && href && href.length > 0)
                    window.location.href = href;
            }
        };
        return ConfirmDialog;
    }());
    Common.ConfirmDialog = ConfirmDialog;
})(Common || (Common = {}));
//# sourceMappingURL=app.js.map