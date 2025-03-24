export interface Brand {
    id: number;
    name: string;
    addedBy: number;
}

export interface addBrandRequest {
    name: string;
    addedBy: number;
}

export interface deleteBrandRequest {
    brandId: number;
}