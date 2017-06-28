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
//# sourceMappingURL=common.js.map