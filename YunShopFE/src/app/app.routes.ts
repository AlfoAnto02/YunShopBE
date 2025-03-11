import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { SignUpComponent } from './components/user-components/sign-up/sign-up.component';
import { SignInComponent } from './components/user-components/sign-in/sign-in.component';
import { ProfileComponent } from './components/user-components/profile/profile.component';
import { AuthGuard } from './guards/auth.guard';
import { CategoriesListComponent } from './components/category-components/categories-list/categories-list.component';
import { NewCategoryComponent } from './components/category-components/new-category/new-category.component';
import { ProductsListComponent } from './components/product-components/products-list/products-list.component';
import { NewProductComponent } from './components/product-components/new-product/new-product.component';
import { NewBrandComponent } from './components/brand-components/new-brand/new-brand.component';
import { NewSizeComponent } from './components/size-components/new-size/new-size.component';
import { SizesListComponent } from './components/size-components/sizes-list/sizes-list.component';
import { BrandsListComponent } from './components/brand-components/brands-list/brands-list.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'SignUp', component: SignUpComponent },
    { path: 'SignIn', component: SignInComponent },
    { path: 'Profile', component: ProfileComponent, canActivate: [AuthGuard] },
    { path: 'Categories', component: CategoriesListComponent },
    { path: 'New-Category', component: NewCategoryComponent, canActivate: [AuthGuard] },
    { path: 'Products', component: ProductsListComponent },
    { path: 'New-Product', component: NewProductComponent, canActivate: [AuthGuard] },
    { path: 'Brands', component: BrandsListComponent, canActivate: [AuthGuard] },
    { path: 'New-Brand', component: NewBrandComponent, canActivate: [AuthGuard] },
    { path: 'Sizes', component: SizesListComponent, canActivate: [AuthGuard] },
    { path: 'New-Size', component: NewSizeComponent, canActivate: [AuthGuard] },
];