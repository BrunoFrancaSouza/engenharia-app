import { Component, OnInit, Output, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  @Output() loginEvent: EventEmitter<any> = new EventEmitter();
  
  options = Object.assign(
    { param1: 'Parâmetro 1' },
    { param2: 'Parâmetro 2' },
  );

  constructor() { }

  ngOnInit() {
    this.loginEvent.emit(this.options)
  }

}
