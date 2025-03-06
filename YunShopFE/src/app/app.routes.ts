import { Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { SignUpComponent } from './components/user-components/sign-up/sign-up.component';
import { SignInComponent } from './components/user-components/sign-in/sign-in.component';
import { ProfileComponent } from './components/user-components/profile/profile.component';
import { AuthGuard } from './guards/auth.guard';
import { CategoriesListComponent } from './components/category-components/categories-list/categories-list.component';
import { NewCategoryComponent } from './components/category-components/new-category/new-category.component';
import { ProductsListComponent } from './components/product-components/products-list/products-list.component';
import { ProductDetailComponent } from './components/product-components/product-detail/product-detail.component';
import { NewProductComponent } from './components/product-components/new-product/new-product.component';

export const routes: Routes = [
    { path: '', component: HomeComponent},
    { path: 'SignUp', component: SignUpComponent },
    { path: 'SignIn', component: SignInComponent },
    { path: 'Profile', component: ProfileComponent, canActivate: [AuthGuard] },
    { path: 'Categories', component: CategoriesListComponent },
    { path: 'New-Category', component: NewCategoryComponent, canActivate: [AuthGuard] },
    { path: 'Products', component: ProductsListComponent },
    { path: 'Products/Product/:id', component: ProductDetailComponent },
    { path: 'New-Product', component: NewProductComponent, canActivate: [AuthGuard] },
];