export interface Category {
    id: number;
    name: string;
    addedById: number;
}

export interface addCategoryRequest {
    name: string;
    addedById: number;
}