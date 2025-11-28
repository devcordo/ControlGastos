import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { PresupuestoService } from '../../core/services/presupuesto.service';
import { TiposGastoService } from '../../core/services/tipos-gasto.service';
import { Presupuesto } from '../../core/models/presupuesto.model';
import { TipoGasto } from '../../core/models/tipo-gasto.model';

@Component({
  selector: 'app-presupuesto',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatIconModule,
    MatTableModule
  ],
  templateUrl: './presupuesto.component.html'
})
export class PresupuestoComponent {
  presupuestos: Presupuesto[] = [];
  tiposGasto: TipoGasto[] = [];

  meses = [
    {value: 0, name: 'Seleccione un mes' },
    {value: 1, name: 'Enero' },
    {value: 2, name: 'Febrero' },
    {value: 3, name: 'Marzo' },
    {value: 4, name: 'Abril' },
    {value: 5, name: 'Mayo' },
    {value: 6, name: 'Junio' },
    {value: 7, name: 'Julio' },
    {value: 8, name: 'Agosto' },
    {value: 9, name: 'Septiembre' },
    {value: 10, name: 'Octubre' },
    {value: 11, name: 'Noviembre' },
    {value: 12, name: 'Diciembre' }
  ];

  anios = [
    2025,2026,2027,2028,2029,2030
  ];

  displayedColumns = ['mes','anio', 'categoria','monto','acciones'];

  editingPresupuesto: Presupuesto = {
    id: 0,
    tipoGastoId: 0,
    anio: 0, 
    mes: 0,
    monto: 0
  };

  constructor(
    private service: PresupuestoService,
    private tiposService: TiposGastoService
  ) {
    this.loadTipos();
  }

  loadTipos() {
    this.service.getAllPresupuestos().subscribe(data => this.presupuestos = data);
    this.tiposService.getTiposGasto().subscribe(data => this.tiposGasto = data);
  }

  cargarPresupuestos() {
    this.service.getPresupuestos(this.editingPresupuesto.mes, this.editingPresupuesto.anio)
      .subscribe(data => this.presupuestos = data);
  }

  guardar() {
    const req = this.editingPresupuesto.id
      ? this.service.actualizar(this.editingPresupuesto)
      : this.service.crear(this.editingPresupuesto);

    req.subscribe(() => {
      this.editingPresupuesto = { id: 0, tipoGastoId: 0, anio: 0, mes: 0, monto: 0 };
      this.cargarPresupuestos();
    });
  }

  editar(item: Presupuesto) {
    this.editingPresupuesto = { ...item };
  }

  eliminar(id: number) {
    if (!id) return;

    this.service.eliminar(id).subscribe(() => {
      this.cargarPresupuestos();
    });
  }

  getNombreMes(numero: number): string {
    return this.meses[numero].name ?? '';
  }

  getCategoria(id: number): string {
        const tipo = this.tiposGasto.find(x => x.id === id);
        return tipo ? tipo.nombre : '';
    }
}
