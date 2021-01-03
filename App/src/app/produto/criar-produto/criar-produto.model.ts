export class CriarProduto {
  constructor(nome: string, valorDeVenda: number, arquivo: File) {
    this.nome = nome;
    this.valorDeVenda = valorDeVenda;
    this.imagem = arquivo;
  }

  nome: string;
  valorDeVenda: number;
  imagem: File;
}
