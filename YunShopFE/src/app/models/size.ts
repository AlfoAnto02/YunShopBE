export interface Size {
    id: number;
    size: string;
    addedBy: number;
}

export interface addSizeRequest {
    size: string;
    addedBy: number;
}

export interface deleteSizeRequest {
    id: number;
    userId: number;
}