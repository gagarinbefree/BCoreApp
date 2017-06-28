/// <reference path="../typings/jquery/jquery.d.ts" />
/// <reference path="../typings/bootbox/index.d.ts" />

module Common {
    export class App {
        private confirm: ConfirmDialog;
        private static editor: AceAjax.Editor;     

        constructor() {
            this.confirm = new ConfirmDialog();
            //App.ace();
        }

        //public static ace(): void {
        //    $("pre").each((i: any, e: Element) => App.initAce(i, e));
        //}

        //private static initAce(i: any, e: Element): void {
        //    this.editor = ace.edit(<HTMLElement>e);
        //    this.editor.setTheme('ace/theme/twilight');
        //    this.editor.renderer.setShowGutter(false);
        //    this.editor.setReadOnly(true);
        //}

        //private static heightUpdateAce(): void {
        //    // http://stackoverflow.com/questions/11584061/
        //    var newHeight: Number =
        //        this.editor.getSession().getScreenLength() * this.editor.renderer.lineHeight + this.editor.renderer.scrollBar.getWidth();
        //    $('#editor').height(newHeight.toString() + "px");
        //    $('#editor-section').height(newHeight.toString() + "px");
        //    // This call is required for the editor to fix all of
        //    // its inner structure for adapting to a change in size
        //    editor.resize();
        //};
    }

    export class Pager {
        private url: string;
        private page: number;
        private elContainer: JQuery;
        private elButtonDone: JQuery;
        private elPagerNumber: JQuery;

        constructor(url, page, elContainer, elButtonDone) {
            this.url = url;
            this.page = Number(page);
            this.elContainer = elContainer;
            this.elButtonDone = elButtonDone;            
            if (this.elButtonDone)
                this.elButtonDone.on('click', (e: JQueryEventObject) => this.loadPage(e));                 
        }

        private loadPage(e: JQueryEventObject): void {
            if (this.elContainer) {   
                this.elContainer.append($('<div>').load(this.url, { page: this.page + 1 }, (responseText: string, textStatus: string, XMLHttpRequest: XMLHttpRequest) => this.init(responseText, textStatus, XMLHttpRequest)));
            }
        }

        private init(responseText: string, textStatus: string, XMLHttpRequest: XMLHttpRequest): void {
            if (textStatus != 'success'
                || XMLHttpRequest.status != 200
                || this.isEmpty(responseText)
                || !this.isHtml(responseText)) {

                this.elButtonDone.hide();
            }
            else
                this.page++;
        }

        private isEmpty(str: string): boolean {
            return str.replace(new RegExp('\r\n', 'g'), '').replace(/\s/g, '').trim().length == 0;
        }

        private isHtml(str: string): boolean {
            return /<[a-z\][\s\S]*>/i.test(str);
        }
    }

    export class ConfirmDialog {
        private elConfirm: JQuery = $('a[data-confirm]');        
        private elTarget: JQuery;

        constructor() {
            this.elConfirm.on('click', (e: JQueryEventObject) => this.showDialog(e));
        }

        private showDialog(e: JQueryEventObject): void {
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
                callback: (result: boolean) => this.handlerDialogResult(result)
            });
        }

        private handlerDialogResult(result: boolean) : void {
            if (result) {
                var href: string = this.elTarget.attr('href');
                if (typeof (href) !== "undefined" && href && href.length > 0)
                    window.location.href = href;
            }
        }
    }
}