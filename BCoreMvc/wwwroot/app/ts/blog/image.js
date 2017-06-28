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
//# sourceMappingURL=image.js.map