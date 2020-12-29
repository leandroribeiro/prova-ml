import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Produto} from "../model/produto.model";
import {Observable} from "rxjs/index";
import {ApiResponse} from "../model/api.response";

@Injectable()
export class ApiService {

  constructor(private http: HttpClient) { }
  baseUrl: string = 'http://localhost:8081/produtos/';

  login(loginPayload) : Observable<ApiResponse> {
    return this.http.post<ApiResponse>('https://dev.sitemercado.com.br/api/login', loginPayload);
  }

  obterProdutos() : Observable<ApiResponse> {
    return this.http.get<ApiResponse>(this.baseUrl);
  }

  obterProdutoPorId(id: number): Observable<ApiResponse> {
    return this.http.get<ApiResponse>(this.baseUrl + id);
  }

  criarProduto(produto: Produto): Observable<ApiResponse> {
    return this.http.post<ApiResponse>(this.baseUrl, produto);
  }

  atualizarProduto(produto: Produto): Observable<ApiResponse> {
    return this.http.put<ApiResponse>(this.baseUrl + produto.id, produto);
  }

  excluirProduto(id: number): Observable<ApiResponse> {
    return this.http.delete<ApiResponse>(this.baseUrl + id);
  }
}