import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { ListarProdutoComponent } from './produto/listar-produto/listar-produto.component';

import {ReactiveFormsModule} from "@angular/forms";
import {ApiService} from "./service/api.service";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import { CriarProdutoComponent } from './produto/criar-produto/criar-produto.component';
import { EditarProdutoComponent } from './produto/editar-produto/editar-produto.component';
// import {TokenInterceptor} from "./core/interceptor";

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    ListarProdutoComponent,
    CriarProdutoComponent,
    EditarProdutoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule    
  ],
  providers: [
    ApiService, 
    // {provide: HTTP_INTERCEPTORS,useClass: TokenInterceptor,multi : true}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
