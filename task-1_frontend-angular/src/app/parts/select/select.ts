import { Component, input } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';

export interface Option {
  label: string;
  value: number;
}

@Component({
  selector: 'app-select',
  imports: [MatSelectModule, ReactiveFormsModule],
  templateUrl: './select.html',
  styleUrl: './select.scss',
})
export class Select {
  allowReset = input<boolean>(false);
  control = input.required<FormControl<number | null>>(); // must not be named formControl to avoid conflict with Angular directive
  label = input.required<string>();
  options = input.required<Option[]>();
}
