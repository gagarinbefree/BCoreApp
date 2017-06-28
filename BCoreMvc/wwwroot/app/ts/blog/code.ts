/// <reference path="../../typings/jquery/jquery.d.ts" />
/// <reference path="../../typings/bootstrap/bootstrap.d.ts" />
/// <reference path="../../typings/highlightjs/highlightjs.d.ts" />
/// <reference path="../../typings/ace/index.d.ts" />

module Blog {
    export class Code {
        private owner: WhatsNew;
        private elModalForm: JQuery = $('#codeModalForm');
        private elButtonUpload: JQuery = $("#codeButtonUpload");       
        private editor: AceAjax.Editor;

        constructor(owner: WhatsNew) {
            this.owner = owner;

            this.init();
        }

        private init(): void {
            this.elModalForm.on('shown.bs.modal', (e: JQueryEventObject) => this.formShow(e));            
            this.elButtonUpload.on('click', (e: JQueryEventObject) => this.submit(e));
        }

        private formShow(e: JQueryEventObject): void {
            this.initAceEditor();            
            this.elButtonUploadDisabled();
        }

        private initAceEditor(): void {
            this.editor = ace.edit('codeText');
            this.editor.setTheme('ace/theme/twilight');
            this.editor.renderer.setShowGutter(false);
            this.editor.focus();
            this.editor.on('change', (e: JQueryEventObject) => this.editorChangeValue(e));
        }

        private elButtonUploadDisabled(): void {
            this.elButtonUpload.prop('disabled'
                , typeof (this.editor) == 'undefined' || this.isEmpty(this.editor.getValue()));           
        }

        private editorChangeValue(e: JQueryEventObject): void {
            this.elButtonUploadDisabled();
        }

        private isEmpty(str: string): boolean {
            return str.replace(new RegExp('\r\n', 'g'), '').replace(/\s/g, '').trim().length == 0;
        }

        private submit(e: JQueryEventObject) {
            this.owner.elCode.val(this.editor.getValue());
            this.owner.post(e);

            setTimeout(() => {
                this.elModalForm.modal('hide');
            }, 250);
        }
    }
}