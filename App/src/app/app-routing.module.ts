import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from './login/login.component';
import {ListarProdutoComponent} from './produto/listar-produto/listar-produto.component';
import {CriarProdutoComponent} from './produto/criar-produto/criar-produto.component';
import { EditarProdutoComponent } from './produto/editar-produto/editar-produto.component';
import {AuthGuard} from './helpers/auth.guard.js';

const routes: Routes = [
  { path: 'login', component: LoginComponent  },
  { path: 'listar-produto', component: ListarProdutoComponent, canActivate: [AuthGuard] },
  { path: 'criar-produto', component: CriarProdutoComponent, canActivate: [AuthGuard] },
  { path: 'editar-produto', component: EditarProdutoComponent, canActivate: [AuthGuard] },
  { path: '', component : ListarProdutoComponent, canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
