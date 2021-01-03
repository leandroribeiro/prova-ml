import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {ApiService} from '../../service/api.service';
import {Produto} from 'src/app/model/produto.model';
import {first} from 'rxjs/operators';
import {EditarProduto} from './editar-produto.model';

@Component({
  selector: 'app-editar-produto',
  templateUrl: './editar-produto.component.html',
  styleUrls: ['./editar-produto.component.css']
})
export class EditarProdutoComponent implements OnInit {

  produto: Produto;
  arquivo: File = null;
  editForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private router: Router, private apiService: ApiService) {
  }

  ngOnInit() {
    const produtoId = window.localStorage.getItem('editarProdutoId');

    if (!produtoId) {
      alert('Invalid action.');
      this.router.navigate(['listar-produto']);
      return;
    }

    this.editForm = this.formBuilder.group({
      id: ['', Validators.required],
      nome: ['', Validators.required],
      valorDeVenda: ['', Validators.required],
      // imagem: ['', Validators.required],
      // imagemRoute: ['', Validators.required]
    });

    this.apiService.obterProdutoPorId(+produtoId)
      .subscribe(data => {
        this.produto = data,
          this.editForm.controls.id.setValue(data.id),
          this.editForm.controls.nome.setValue(data.nome),
          this.editForm.controls.valorDeVenda.setValue(data.valorDeVenda);
        // this.editForm.setValue(data);
        // this.editForm.setValue(data.result);
      });
  }

  handleFileInput(files: FileList) {
    this.arquivo = files.item(0);
  }

  onSubmit() {
    const model = new EditarProduto(this.editForm.controls.id.value,
      this.editForm.controls.nome.value,
      this.editForm.controls.valorDeVenda.value,
      this.arquivo);

    this.apiService.atualizarProduto(model)
      .pipe(first())
      .subscribe(
        data => {
          // if(data.status === 200) {
          this.router.navigate(['listar-produto']);
          // this.router.navigate(['listar-produto'], {queryParams: {refresh: new Date().getTime()}});
          // }else {
          // alert(data.message);
          // }
        },
        error => {
          console.error(error);
        });
  }

}
