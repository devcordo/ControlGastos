import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegistroGastosService } from '../../core/services/registro-gastos.service';
import { RegistroGastosService as GastoService } from '../../core/services/registro-gastos.service';
import { Gasto, GastoDetalle } from '../../core/models/gasto.model';
import { TiposGastoService } from '../../core/services/tipos-gasto.service';
import { TipoGasto } from '../../core/models/tipo-gasto.model';
import { FondoMonetarioService } from '../../core/services/fondo-monetario.service';
import { FondoMonetario } from '../../core/models/fondo-monetario.model';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';


@Component({
  selector: 'app-registro-gastos',
  standalone: true,
  imports: [
    CommonModule, 
    FormsModule, 
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatTableModule,
    MatCardModule,
    MatIconModule,
    MatDatepickerModule,
    MatNativeDateModule],
  templateUrl: './registro-gastos.component.html'
})
export class RegistroGastosComponent {
  gastoForm: FormGroup;
  detalleForm: FormGroup;
  gasto: Gasto = { id: 0, tipo: '', fecha: new Date(), fondoMonetarioId: 0, observaciones: '', nombreComercio: '', tipoDocumento: 'Factura', detalles: [] };
  tiposGasto: TipoGasto[] = [];
  selectedGastoId!: number;
  fondos: FondoMonetario[] = [];
  selectedFondoId!: number;
  detalle: GastoDetalle = { tipoGastoId: 0, monto: 0 };

  tiposDocumento = ['Comprobante','Factura','Otro'];
  columnasDetalle = ['tipoGasto', 'monto', 'acciones'];

  get detallesTabla() {
    return this.gasto.detalles.map(d => ({
        ...d,
        nombreTipoGasto: this.tiposGasto.find(t => t.id === d.tipoGastoId)?.nombre || ''
    }));
  }

  constructor(private fb: FormBuilder, private service: GastoService, private fondosService: FondoMonetarioService, private tiposGastoService: TiposGastoService) {
    this.gastoForm = this.fb.group({
    fecha: [new Date(), Validators.required],
    fondoMonetarioId: [null, Validators.required],
    nombreComercio: ['', Validators.required],
    tipoDocumento: ['Comprobante'],
    observaciones: ['']
  });

    this.detalleForm = this.fb.group({
      tipoGastoId: [null, Validators.required],
      monto: [0, Validators.required]
    });

    // Cargar combos
    this.cargarTiposGasto();
    this.cargarFondos();
  }

  cargarTiposGasto() { this.tiposGastoService.getTiposGasto().subscribe({
        next: (data) => { this.tiposGasto = data; },
        error: (err) => console.error('Error al cargar tipos de gasto', err)
    });
  }
  cargarFondos() { this.fondosService.getFondoMonetario().subscribe({
        next: (data) => { this.fondos = data; },
        error: (err) => console.error('Error al cargar fondos monetarios', err)
    }); 
  }


  agregarDetalle() {
    if (this.detalleForm.invalid) {
        this.detalleForm.markAllAsTouched();
        return;
    }
    const nuevoDetalle: GastoDetalle = {
        tipoGastoId: this.detalleForm.value.tipoGastoId,
        monto: this.detalleForm.value.monto
    };
    this.gasto.detalles.push(nuevoDetalle);

    this.detalleForm.reset({ tipoGastoId: null, monto: 0 });
  }

  eliminarDetalle(index: number) {
    this.gasto.detalles.splice(index,1);
  }

  guardar() {
    if (this.gastoForm.invalid) {
        this.gastoForm.markAllAsTouched();
        return;
    }

    if (this.gasto.detalles.length === 0) {
        alert('Debe agregar al menos un detalle al gasto.');
        return;
    }

    const gastoAGuardar= this.gastoForm.value;

    this.gasto = {
        ...this.gasto,
        fecha: gastoAGuardar.fecha,
        fondoMonetarioId: gastoAGuardar.fondoMonetarioId,
        nombreComercio: gastoAGuardar.nombreComercio,
        tipoDocumento: gastoAGuardar.tipoDocumento,
        observaciones: gastoAGuardar.observaciones
    };

    this.service.crear(this.gasto).subscribe({
        next: () => {
            this.gasto.detalles = [];
            alert('Gasto registrado con éxito.');
        },
        error: (err) => {
            alert(err.error?.message || 'Error al registrar gasto.');
        }
    });
  }
}