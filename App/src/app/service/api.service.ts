import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Produto} from '../model/produto.model';
import {Observable} from 'rxjs';
import {ApiResponse} from '../model/api.response';
import {ListarProduto} from '../produto/listar-produto/listar-produto.model';
import {CriarProduto} from '../produto/criar-produto/criar-produto.model';
import { EditarProduto } from '../produto/editar-produto/editar-produto.model';

@Injectable()
export class ApiService {

  constructor(private http: HttpClient) { }
  baseUrl = 'http://localhost:5000/produtos/';

  login(loginPayload): Observable<ApiResponse> {
    return this.http.post<ApiResponse>('https://dev.sitemercado.com.br/api/login', loginPayload);
  }

  obterProdutos(): Observable<ListarProduto[]> {
    return this.http.get<ListarProduto[]>(this.baseUrl);
  }

  obterProdutoPorId(id: number): Observable<Produto> {
    return this.http.get<Produto>(this.baseUrl + id);
  }

  criarProduto(produto: CriarProduto): Observable<ListarProduto> {
    const formData = new FormData();
    formData.append('nome', produto.nome);
    formData.append('valorDeVenda', produto.valorDeVenda.toString());
    formData.append('imagem', produto.imagem);

    return this.http.post<ListarProduto>(this.baseUrl, formData);
  }

  atualizarProduto(produto: EditarProduto): Observable<Produto> {
    const formData = new FormData();
    formData.append('nome', produto.nome);
    formData.append('valorDeVenda', produto.valorDeVenda.toString());
    formData.append('imagem', produto.imagem);

    return this.http.put<Produto>(this.baseUrl + produto.id, formData);
  }

  excluirProduto(id: number): Observable<ApiResponse> {
    return this.http.delete<ApiResponse>(this.baseUrl + id);
  }
}
