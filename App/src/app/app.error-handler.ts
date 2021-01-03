import {HttpErrorResponse} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';

export class ErrorHandler {
  static handleError(error: HttpErrorResponse | any) {
    let errorMessage: string;
    if (error.error instanceof ErrorEvent) {
      // Erro ocorreu no lado do client
      errorMessage = error.error.message;
      // errorMessage = `Erro ${error.status} ao acessar a URL ${error.url} - ${error.statusText}`;
    } else {
      // Erro ocorreu no lado do servidor
      errorMessage = `Código do erro: ${error.status}, ` + `mensagem: ${error.message}`;
      // errorMessage = error.toString();
    }

    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
