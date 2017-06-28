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
            }
        };
        return Comment;
    }());
    Post.Comment = Comment;
})(Post || (Post = {}));
