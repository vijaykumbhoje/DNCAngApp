import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_service/auth.service';
import { AlertifyService } from '../_service/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(public authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
       this.alertify.success('Login Successful');
    }, error => {
      this.alertify.error('Login Failed');
      console.log(error);
    }, () => {
      this.router.navigate(['/members']);
    }
    );
  }

  loggedIn() {
    //  const token = localStorage.getItem('token');
    //  return !!token;
    return this.authService.loggedIn();
    }

  logout() {
     localStorage.removeItem('token');
     this.alertify.message('Logged out');
     this.router.navigate(['/home']);
  }


}
