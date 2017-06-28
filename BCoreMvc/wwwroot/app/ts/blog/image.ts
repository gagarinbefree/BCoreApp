/// <reference path="../../typings/jquery/jquery.d.ts" />
/// <reference path="../../typings/jquery.fileupload/jquery.fileupload.d.ts" />
/// <reference path="../../typings/nprogress/nprogress.d.ts" />
/// <reference path="../../typings/bootstrap/bootstrap.d.ts" />
/// <reference path="../../typings/imagefill.js/imagefill.js.d.ts" />

module Blog {
    export class Image {
        private owner: WhatsNew;

        private elBrowseText: JQuery = $("#imageBrowseText");
        private elPreview: JQuery = $("#imageUploadPreview");
        private elBrowseButton: JQuery = $("#imageBrowseBtnUpload");
        private elInput: JQuery = $("#imageInputUpload");
        private elButtonUpload: JQuery = $("#imageButtonUpload");        
        private elFileName: JQuery = $("#imageFileNameUpload");
        private elModalForm: JQuery = $("#imageModalForm");
                
        private url: string;
        private reader: FileReader = new FileReader();
        private file: JQueryFileUpload;

        private dataSubmit: JQueryFileUploadChangeObject = null;

        constructor(owner: WhatsNew) {
            this.owner = owner;
            this.url = "/FileHandler/Upload?objectContext=" + this.owner.userId;

            this.init();            
        }

        private init(): void {
            this.elButtonUpload.prop("disabled", true);               

            NProgress.configure({
                parent: "#imageProgressUpload"
                , showSpinner: false
            });

            this.elInput.fileupload({
                url: this.url
                , dataType: 'json'
                , autoUpload: false
                , singleFileUploads: true
                , acceptFileTypes: /(\.|\/)(gif|jpe?g|png)$/i
                , add: (e: JQueryEventObject, data: JQueryFileUploadChangeObject) => this.addDataSubmit(e, data)
                , start: () =>  this.startUpload()
                , done: (e: Event, data: any) => this.doneUpload(e, data)
            });

            this.elBrowseButton.on('click', (e: JQueryEventObject) => this.browse(e));
            this.elButtonUpload.on('click', (e: JQueryEventObject) => this.submit(e));

            this.elInput.on('change', (e: JQueryEventObject) => this.inputChange(e));            
            this.reader.onload = (e: ProgressEvent) => this.onLoad(e);                                    
        }

        private browse(e: JQueryEventObject): void {
            this.dataSubmit = null;
            this.elInput.html(this.elInput.html());
        }

        private inputChange(e: JQueryEventObject): void {
            var el: HTMLInputElement = <HTMLInputElement>e.currentTarget;
            var path = el.value;
            this.elFileName.val(path.replace(/^.*[\\\/]/, ''));
            this.reader.readAsDataURL(el.files[0]);
        }

        private submit(e: JQueryEventObject): void {
            if (this.dataSubmit != null)
                this.dataSubmit.submit();        
        }

        private onLoad(e: ProgressEvent): void {
            this.elPreview.attr("src", this.reader.result);
            this.elButtonUpload.prop("disabled", false);

            this.elBrowseText.hide();
        }

        private startUpload(): void {
            NProgress.start();
        }        

        private addDataSubmit(e: JQueryEventObject, data: JQueryFileUploadChangeObject): void {
            this.dataSubmit = data;
        }

        private doneUpload(e: Event, data: any): void {
            NProgress.done(true);

            this.owner.elImageUrl.val(data.result.files.length > 0 ? data.result.files[0].url : "");
            this.owner.post(e);

            setTimeout(() => {
                this.elModalForm.modal("hide");                
            }, 250);
        }        
    }
}