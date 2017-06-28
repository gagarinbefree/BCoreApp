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
