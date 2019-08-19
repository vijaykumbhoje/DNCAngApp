import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/_models/user';
import { UserService } from 'src/app/_service/user.service';
import { AlertifyService } from 'src/app/_service/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryOptions, NgxGalleryImage, NgxGalleryAnimation } from 'ngx-gallery';

@Component({
  selector: 'app-member-details',
  templateUrl: './member-details.component.html',
  styleUrls: ['./member-details.component.css']
})
export class MemberDetailsComponent implements OnInit {
  user: User;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];

  constructor(private userService: UserService, private alertify: AlertifyService,
              private route: ActivatedRoute) { }

  ngOnInit() {
   this.route.data.subscribe(data => {
     // tslint:disable-next-line:no-string-literal
     this.user = data.user;
   });

   this.galleryOptions =  [
     {
       width: '300px',
       height: '3 00px',
       thumbnailsColumns: 4,
       imagePercent: 100,
       imageAnimation: NgxGalleryAnimation.Slide,
       preview: false
     }
   ];

   this.galleryImages = this.getimages();
  }

  getimages() {
    const imageUrls = [];
    // tslint:disable-next-line:prefer-for-of
    for (let i = 0; i < this.user.photos.length; i++) {
      imageUrls.push({
        small: this.user.photos[i].url,
        medium: this.user.photos[i].url,
        big: this.user.photos[i].url,
        description: this.user.photos[i].url
      });
    }
    return imageUrls;
  }
}

  // loadUser() {
  //     // tslint:disable-next-line:no-string-literal
  //     this.userService.getUser(+this.route.snapshot.params.id).subscribe((user: User) => {
  //     this.user = user;
  //   }, error => {
  //     this.alertify.error(error);
  //   });
  // }


