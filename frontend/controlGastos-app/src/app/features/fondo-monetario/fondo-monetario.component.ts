import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { FondoMonetarioService } from '../../core/services/fondo-monetario.service';
import { FondoMonetario } from '../../core/models/fondo-monetario.model';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-fondo-monetario',
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
    MatTableModule],
  templateUrl: './fondo-monetario.component.html'
})
export class FondoMonetarioComponent {
    fondos: FondoMonetario[] = [];
    editingFondo: FondoMonetario = { id: 0, nombre: '', tipo: 'Cuenta Bancaria' };

    displayedColumns = ['nombre', 'tipo', 'acciones'];

    constructor(private service: FondoMonetarioService) {
        this.loadFondos();
    }

    loadFondos() {
        this.service.getFondoMonetario().subscribe(data => this.fondos = data);
    }

    guardar() {
        if (this.editingFondo.id) {
            this.service.actualizar(this.editingFondo).subscribe(() => this.loadFondos());
        } else {
            this.service.crear(this.editingFondo).subscribe(() => this.loadFondos());
        }
    }

    eliminar(id: number) {
        if (id != null) {
            this.service.eliminar(id).subscribe(() => this.loadFondos());
        }
    }   
}