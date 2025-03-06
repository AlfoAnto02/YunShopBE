import { CommonModule } from '@angular/common';
import { Component, AfterViewInit } from '@angular/core';
import { Router, RouterLink, RouterOutlet } from '@angular/router';
import { CategoriesService } from './services/category.service';
import { UserService } from './services/user.service';
import { TokenService } from './services/token.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, RouterLink],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements AfterViewInit {
  title = 'Catalog';
  isPopupVisible = false;
  user : any = null;
  isAuthenticated = false;

  constructor(private CategoriesService: CategoriesService, private router: Router, private usersService : UserService, private tokenService: TokenService) {}

  ngOnInit(): void {
      this.checkAuthentication();
    }

  ngAfterViewInit(): void {
    const bar = Array.from(document.querySelectorAll("li"));

    bar.forEach(function(it) {
      it.onclick = function() {
        bar.forEach(function(el) {
          el.classList.remove("active");
        });
        (this as HTMLElement).classList.toggle("active");
      };
    });
  }

  onSearchKeyDown(event: KeyboardEvent): void {
    if (event.key === 'Enter') {
      const query = (<HTMLInputElement>event.target).value;
      this.search(query);
    }
  }

  search(query: string): void {
    if (query.trim()) {
      this.router.navigate(['/Products/Search', query]);
    } else {
      this.router.navigate(['/Products']);
    }
  }

  openPopup() {
    this.isPopupVisible = true;
  }

  closePopup() {
    this.isPopupVisible = false;
  }

  checkAuthentication(): void {
    this.isAuthenticated = this.usersService.isAuthenticated();
    if (this.isAuthenticated) {
      const token = this.tokenService.getToken();
      if (token) {
        this.user = this.tokenService.decodeToken(token);
        console.log('User:', this.user);
      }
    }
  }

  logout(): void {
    this.usersService.logout();
    this.user = null;
    this.isAuthenticated = false;
    this.router.navigateByUrl('').then(() => {
      window.location.reload();
    });
  }
}
