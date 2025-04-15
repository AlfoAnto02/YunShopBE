export interface Size {
    id: number;
    sizeValue: string;
    userId: number;
}

export interface addSizeRequest {
    sizeValue: string;
    addedBy: number;
}

export interface deleteSizeRequest {
    sizeId: number;
}