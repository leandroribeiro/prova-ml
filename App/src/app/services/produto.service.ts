import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Produto} from '../models/produto';
import {Observable} from 'rxjs';
import {ApiResponse} from '../models/api.response';
import {CriarProduto} from '../produto/criar-produto/criar-produto.model';
import {EditarProduto} from '../produto/editar-produto/editar-produto.model';
import {environment} from '../../environments/environment';

@Injectable()
export class ProdutoService {

  constructor(private http: HttpClient) {
  }

  obterProdutos(): Observable<Produto[]> {
    return this.http.get<Produto[]>(`${environment.apiUrl}/produtos`);
  }

  obterProdutoPorId(id: number): Observable<Produto> {
    return this.http.get<Produto>(`${environment.apiUrl}/produtos/${id}`);
  }

  criarProduto(produto: CriarProduto): Observable<Produto> {
    const formData = new FormData();
    formData.append('nome', produto.nome);
    formData.append('valorDeVenda', produto.valorDeVenda.toString());
    formData.append('imagem', produto.imagem);

    return this.http.post<Produto>(`${environment.apiUrl}/produtos`, formData);
  }

  atualizarProduto(produto: EditarProduto): Observable<Produto> {
    const formData = new FormData();
    formData.append('nome', produto.nome);
    formData.append('valorDeVenda', produto.valorDeVenda.toString());
    formData.append('imagem', produto.imagem);

    return this.http.put<Produto>(`${environment.apiUrl}/produtos/${produto.id}`, formData);
  }

  excluirProduto(id: number): Observable<ApiResponse> {
    return this.http.delete<ApiResponse>(`${environment.apiUrl}/produtos/${id}`);
  }
}
