// Definitions: https://libraries.io/bower/simple-uuid

declare namespace UUID {
    interface UUIDStatic {        
        generate(): string;
    }   
}

declare var UUID: UUID.UUIDStatic;

declare module 'UUID' {
    export = UUID;
}