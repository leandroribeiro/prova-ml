export class Produto {

  
    id: number;
    nome: string;
    valorDeVenda: number;
    imagem: string;
    imagem2: string;

    get imagemRoute(): string {
      return `http://localhost:8081 + ${this.imagem}`;
    }

    constructor() {
      this.imagem2 = `http://localhost:8081 + ${this.imagem}`;
    }
  
  }