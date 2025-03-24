export interface Size {
    id: number;
    sizeValue: string;
    addedBy: number;
}

export interface addSizeRequest {
    sizeValue: string;
    addedBy: number;
}

export interface deleteSizeRequest {
    id: number;
    userId: number;
}