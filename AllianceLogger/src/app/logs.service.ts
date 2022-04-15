import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';

export class Logs{
  constructor(
    public logID: number,
    public application: string,
    public applicationVersion: string,
    public customerId: number,
    public logDateTime: string,
    public LogContent: string
  ){}
}

@Injectable({
  providedIn: 'root'
})
export class LogsService implements OnInit {
  
  loglist: Logs[] = [];
  private url = "https://api.jsonbin.io/b/6250885bd20ace068f959856/4"
  
  constructor(private http: HttpClient) { }

  getLogs(){
    return this.http.get(this.url); //returns observable 
  }


  ngOnInit(): void {
    
  }


}
