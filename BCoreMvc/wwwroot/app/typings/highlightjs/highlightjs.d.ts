// Type definitions for highlight.js v7.5.0
// Project: https://github.com/isagalaev/highlight.js
// Definitions by: Niklas Mollenhauer <https://github.com/nikeee/>
// Definitions: https://github.com/DefinitelyTyped/DefinitelyTyped

declare namespace hljs {
    var LANGUAGES: { [name: string]: any; };

    function blockText(block: Node, ignoreNewLines: boolean): string;
    function blockLanguage(block: Node): string;

    function highlight(language_name: string, value: string): IHighlightResult
    function highlightAuto(text: string): IHighlightResult

    function fixMarkup(value: string, tabReplace: boolean, useBR: boolean): string;

    function highlightBlock(block: Node, tabReplace?: boolean, useBR?: boolean): void;

    function initHighlighting(): void;
    function initHighlightingOnLoad(): void;

    var tabReplace: string;

    // Common regexps
    var IDENT_RE: string;
    var UNDERSCORE_IDENT_RE: string;
    var NUMBER_RE: string;
    var C_NUMBER_RE: string;
    var BINARY_NUMBER_RE: string;
    var RE_STARTERS_RE: string;

    // Common modes
    var BACKSLASH_ESCAPE: IMode;
    var APOS_STRING_MODE: IMode;
    var QUOTE_STRING_MODE: IMode;
    var C_LINE_COMMENT_MODE: IMode;
    var C_BLOCK_COMMENT_MODE: IMode;
    var HASH_COMMENT_MODE: IMode;
    var NUMBER_MODE: IMode;
    var C_NUMBER_MODE: IMode;
    var BINARY_NUMBER_MODE: IMode;

    interface IHighlightResult {
        relevance: number;
        keyword_count: number;
        value: string;
    }

    interface IAutoHighlightResult extends IHighlightResult {
        language: string;
        second_best?: IAutoHighlightResult;
    }

    // Reference:
    // https://github.com/isagalaev/highlight.js/blob/master/docs/reference.rst
    interface IMode {
        className?: string;
        begin: string;
        end?: string;
        beginWithKeyword?: boolean;
        endsWithParent?: boolean;
        lexems?: string;
        keywords?: Object;
        illegal?: string;
        excludeBegin?: boolean;
        excludeEnd?: boolean;
        returnBegin?: boolean;
        returnEnd?: boolean;
        contains?: IMode[];
        starts?: string;
        subLanguage?: string;
        relevance?: number;
    }
}

declare module "highlightjs"
{
    export = hljs;
}