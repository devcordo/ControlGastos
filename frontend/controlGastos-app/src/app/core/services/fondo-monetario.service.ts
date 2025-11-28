import { Injectable } from "@angular/core";
import { ApiService } from "./api.service";
import { Observable } from "rxjs";
import { FondoMonetario } from "../models/fondo-monetario.model";

@Injectable({ providedIn: 'root' })
export class FondoMonetarioService {
    constructor(private api: ApiService) {}

    getFondoMonetario(): Observable<FondoMonetario[]> {
        return this.api.get<FondoMonetario[]>('Fondos');
    }

    crear(fondo: FondoMonetario): Observable<FondoMonetario> {
        return this.api.post<FondoMonetario>('Fondos', fondo);
    }

    actualizar(fondo: FondoMonetario): Observable<FondoMonetario> {
        return this.api.put<FondoMonetario>(`Fondos/${fondo.id}`, fondo);
    }

    eliminar(id: number): Observable<void> {
        return this.api.delete<void>(`Fondos/${id}`);
    }
}