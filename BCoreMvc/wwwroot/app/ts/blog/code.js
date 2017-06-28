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
//# sourceMappingURL=code.js.map