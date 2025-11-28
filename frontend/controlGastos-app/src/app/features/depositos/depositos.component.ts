import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { DepositosService } from '../../core/services/depositos.service';
import { Deposito } from '../../core/models/deposito.model';
import { FondoMonetarioService } from '../../core/services/fondo-monetario.service';
import { FondoMonetario } from '../../core/models/fondo-monetario.model';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTableModule } from '@angular/material/table';
import { RegistroGastosService } from '../../core/services/registro-gastos.service';
import { validateHorizontalPosition } from '@angular/cdk/overlay';

@Component({
  selector: 'app-depositos',
  standalone: true,
  imports: [
    CommonModule, 
    FormsModule, 
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatSelectModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatTableModule],
  templateUrl: './depositos.component.html'
})
export class DepositosComponent {
  depositos: Deposito[] = [];
  presupuestoForm: FormGroup;
  deposito: Deposito = { id: 0, fecha: new Date(), fondoMonetarioId: 0, monto: 0 };
  fondos: FondoMonetario[] = [];
  movimientos: (Deposito)[] = [];

  displayedColumns = ['fecha', 'fondo', 'monto', 'acciones'];

  constructor(
    private fb: FormBuilder,
    private service: DepositosService, 
    private fondoService: FondoMonetarioService
  ) 
  {
    this.presupuestoForm = this.fb.group({
      fecha: [new Date()],
      fondo: [null],
      monto: [0]
    });
    this.fondoService.getFondoMonetario().subscribe(data => this.fondos = data);
    this.loadDepositos();
  }

  loadDepositos() {
      this.service.getAllDepositos().subscribe(data => this.depositos = data);
  }

  guardar() {    
    const fondo = this.fondos.find(f => f.id === this.deposito.fondoMonetarioId);
    if (!fondo) {
      alert("Fondo monetario no seleccionado o inválido");
      return;
    }

    this.service.crear(this.deposito, fondo).subscribe(() => this.loadDepositos());
    this.presupuestoForm.reset({fondo: null, monto: 0});
  }
  
  getNombreFondo(id: number){
    const fondo = this.fondos.find(x => x.id === id);
    return fondo?.nombre;
  }
}