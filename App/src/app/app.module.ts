import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {ReactiveFormsModule} from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import {ProdutoService} from './services/produto.service';
import {AuthenticationService} from './services/authentication.service';

import {AppComponent} from './app.component';
import {AppRoutingModule} from './app-routing.module';

import {ErrorInterceptor} from './helpers/error.interceptor';
import {JwtInterceptor} from './helpers/jwt.interceptor';

import {LoginComponent} from './login/login.component';
import {ListarProdutoComponent} from './produto/listar-produto/listar-produto.component';
import {CriarProdutoComponent} from './produto/criar-produto/criar-produto.component';
import {EditarProdutoComponent} from './produto/editar-produto/editar-produto.component';

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
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    ProdutoService,
    AuthenticationService
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
