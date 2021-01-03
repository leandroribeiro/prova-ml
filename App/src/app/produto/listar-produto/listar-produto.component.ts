import { Component, OnInit , Inject} from '@angular/core';
import {Router} from '@angular/router';
import {Produto} from '../../model/produto.model';
import {ApiService} from '../../service/api.service';

@Component({
  selector: 'app-listar-produto',
  templateUrl: './listar-produto.component.html',
  styleUrls: ['./listar-produto.component.css']
})
export class ListarProdutoComponent implements OnInit {

  produtos: any;

  constructor(private router: Router, private apiService: ApiService) { }

  ngOnInit() {
    // if(!window.localStorage.getItem('token')) {
      // this.router.navigate(['login']);
      // return;
    // }
    this.apiService.obterProdutos()
      .subscribe( data => {
        this.produtos = data;
      });
  }

  excluirProduto(produto: Produto): void {
    this.apiService.excluirProduto(produto.id)
      .subscribe( data => {
        this.produtos = this.produtos.filter(u => u !== produto);
      });
  }

  editarProduto(produto: Produto): void {
    console.log(produto);
    window.localStorage.removeItem('editarProdutoId');
    window.localStorage.setItem('editarProdutoId', produto.id.toString());
    this.router.navigate(['editar-produto']);
  }

  adicionarProduto(): void {
    this.router.navigate(['criar-produto']);
  }
  
}
