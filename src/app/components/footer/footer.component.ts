import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'login-footer',
  templateUrl: './footer.component.html',
  styleUrls: ['./footer.component.css']
})
export class FooterComponent implements OnInit {
  year = new Date().getFullYear();

  constructor() { }

  ngOnInit(): void {
  }

}
