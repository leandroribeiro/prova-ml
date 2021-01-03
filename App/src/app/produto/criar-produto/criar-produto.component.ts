import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Router} from '@angular/router';
import {ApiService} from '../../service/api.service';
import {CriarProduto} from './criar-produto.model';

@Component({
  selector: 'app-criar-produto',
  templateUrl: './criar-produto.component.html',
  styleUrls: ['./criar-produto.component.css']
})
export class CriarProdutoComponent implements OnInit {

  arquivo: File = null;

  constructor(private formBuilder: FormBuilder, private router: Router, private apiService: ApiService) { }

  addForm: FormGroup;

  ngOnInit() {
    this.addForm = this.formBuilder.group({
      nome: ['', Validators.required],
      valorDeVenda: ['', Validators.required],
      imagem: ['', Validators.required]
    });

  }

  handleFileInput(files: FileList) {
    this.arquivo = files.item(0);
  }

  onSubmit() {
    const model = new CriarProduto(this.addForm.get('nome').value,
      this.addForm.get('valorDeVenda').value,
      this.arquivo);

    this.apiService.criarProduto(model)
      .subscribe( data => {
        this.router.navigate(['listar-produto']);
      });
  }

}
