import { Injectable } from "@angular/core";
import { ApiService } from "./api.service";
import { Observable } from "rxjs";
import { TipoGasto } from "../models/tipo-gasto.model";

@Injectable({ providedIn: 'root' })
export class TiposGastoService {
    constructor(private api: ApiService) {}
    
    getTiposGasto(): Observable<TipoGasto[]> {
        return this.api.get<TipoGasto[]>('TiposGasto');
    }

    crear(tipo: TipoGasto): Observable<TipoGasto> {
        return this.api.post<TipoGasto>('TiposGasto', tipo);
    }

    actualizar(tipo: TipoGasto): Observable<TipoGasto> {
        return this.api.put<TipoGasto>(`TiposGasto/${tipo.id}`, tipo);
    }

    eliminar(id: number): Observable<void> {
        return this.api.delete<void>(`TiposGasto/${id}`);
    }
}