export interface Category {
    id: number;
    name: string;
    addedBy: number;
}

export interface addCategoryRequest {
    Name: string;
    AddedBy: number;
}

export interface deleteCategoryRequest {
    name: string;
    userId: number;
}