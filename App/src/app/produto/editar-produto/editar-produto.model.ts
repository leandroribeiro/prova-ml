export class EditarProduto {
    constructor(id: number, nome: string, valorDeVenda: number, arquivo: File) {
        this.id = id;
        this.nome = nome;
        this.valorDeVenda = valorDeVenda;
        this.imagem = arquivo;
    }

    id: number;
    nome: string;
    valorDeVenda: number;
    imagem: File;
  }
