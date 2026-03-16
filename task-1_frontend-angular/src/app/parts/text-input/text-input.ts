import { Component, input } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-text-input',
  imports: [MatInputModule, ReactiveFormsModule],
  templateUrl: './text-input.html',
  styleUrl: './text-input.scss',
})
export class TextInput {
  control = input.required<FormControl<string | null>>(); // must not be named formControl to avoid conflict with Angular directive
  label = input.required<string>();
  maxLength = input<number | null>(null);
  multiline = input<boolean>(false);
}
