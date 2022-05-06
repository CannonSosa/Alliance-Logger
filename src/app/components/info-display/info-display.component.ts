import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-info-display',
  templateUrl: './info-display.component.html',
  styleUrls: ['./info-display.component.css']
})
export class InfoDisplayComponent implements OnInit {
  logID = 1;
  dateTime= "10-22-22 t10:52:23.3299432"

  constructor() { }

  ngOnInit(): void {
  }

}
