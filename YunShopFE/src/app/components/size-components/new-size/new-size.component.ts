import { CommonModule } from '@angular/common';
import { Component, HostListener } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { addSizeRequest } from '../../../models/size';
import { Router } from '@angular/router';
import { SizeService } from '../../../services/size.service';
import { TokenService } from '../../../services/token.service';

@Component({
  selector: 'app-new-size',
  imports: [CommonModule, FormsModule],
  templateUrl: './new-size.component.html',
  styleUrl: './new-size.component.scss'
})
export class NewSizeComponent {
  sizeValue: string = '';
  currentUserId: number = 0;
  addSizeRequest: addSizeRequest = {
    sizeValue: '',
    addedBy: 0
  };

  constructor(private router: Router, private SizeService: SizeService,
    private TokenService: TokenService) { }

  ngOnInit(): void {
    this.getCurrentUserId();
  }

  getCurrentUserId() {
    const token = this.TokenService.getToken();
    if (token) {
      const decodedPayload = this.TokenService.decodeToken(token);
      this.currentUserId = decodedPayload.user_id;
    }
  }

  createAddSizeRequest(): void {
    this.addSizeRequest =
    {
      sizeValue: this.sizeValue,
      addedBy: this.currentUserId
    };
  }

  onSubmit() {
    this.createAddSizeRequest()
    console.log('Creating Size:', this.addSizeRequest);
    this.SizeService.addSize(this.addSizeRequest)
      .subscribe({
        next: response => {
          console.log('Size created successfully:', response);
          alert('Size created successfully');
          this.sizeValue = '';
          this.router.navigate(['/New-Size']);
        },
        error: error => {
          console.error('Error creating Size:', error);
          alert('Error creating Size: ' + (error.error?.message || 'Unknown error'));
          this.sizeValue = '';
        }
      });
  }

  @HostListener('document:keydown.escape', ['$event'])
  handleEscape(event: KeyboardEvent) {
    this.close();
  }

  close() {
    this.router.navigate(['/Sizes']);
  }
}
