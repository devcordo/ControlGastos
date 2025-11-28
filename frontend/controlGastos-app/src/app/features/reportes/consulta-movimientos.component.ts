import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegistroGastosService } from '../../core/services/registro-gastos.service';
import { DepositosService } from '../../core/services/depositos.service';
import { Gasto } from '../../core/models/gasto.model';
import { Deposito } from '../../core/models/deposito.model';
import { MatButtonModule } from '@angular/material/button';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-consulta-movimientos',
  standalone: true,
  imports: [
    CommonModule, 
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatButtonModule,
    MatTableModule],
  templateUrl: './consulta-movimientos.component.html'
})
export class ConsultaMovimientosComponent {
  fechaInicio: Date = new Date();
  fechaFin: Date = new Date();
  movimientos: (Gasto | Deposito)[] = [];

  columnas: string[] = ['fecha', 'tipo', 'fondoMonetario', 'nombreComercio', 'tipoDocumento', 'monto'];

  constructor(private gastosService: RegistroGastosService) {}

  buscar() {
    this.movimientos = [];
    this.gastosService.getGastos(this.fechaInicio.toISOString(), this.fechaFin.toISOString()).subscribe(data => {
        this.movimientos = [...data];
    });
  }
}