import { IPagination } from './models/pagination';
import { IProduct } from './models/product';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  Products:IProduct[]=[]
  constructor(private http:HttpClient){}
  ngOnInit(): void {
   this.http.get('https://localhost:5001/api/products/').subscribe((response:any)=>{
     this.Products = response.data     
   },error=>{
     console.log(error);
   })
  }

  title = 'Skinet';
}
