import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class PostLogsService {
  private url = "https://api.jsonbin.io/b/6250885bd20ace068f959856/17";

  constructor(private http: HttpClient) { }

  getLogs(){
    //returns observable
    return this.http.get(this.url);
  }

  // updateLog(log:any){
  //   return this.http.put(this.url + '/' + log.id, log)
  // }

  // deleteLog(id:any){
  //   return this.http.delete(this.url + '/' + id)
  // }
}
