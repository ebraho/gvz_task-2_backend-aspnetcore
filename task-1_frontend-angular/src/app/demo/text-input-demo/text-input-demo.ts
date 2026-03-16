import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import { PartDemo } from '../part-demo';
import { TextInput } from '../../parts/text-input/text-input';

@Component({
  selector: 'app-text-input-demo',
  imports: [PartDemo, TextInput],
  templateUrl: './text-input-demo.html',
  styleUrls: ['./text-input-demo.scss', '../part-demo-common.scss'],
})
export class TextInputDemo {
  protected label = 'Texteingabe';
  protected description = [
    'Eingabe von Text.',
    'Text kann einzeilig oder mehrzeilig sein. Eine maximale Länge kann festgelegt werden.',
  ];
  protected parameters = [
    { name: 'Label', valueRange: '<Text>', required: true },
    { name: 'Maximale Länge', valueRange: '<Zahl>', required: false },
    {
      name: 'Mehrzeilig',
      valueRange: 'Ja oder Nein',
      required: false,
      defaultValue: 'Nein',
    },
  ];

  protected textOneLineFormControl = new FormControl<string | null>('');
  protected textOneLineConstrainedFormControl = new FormControl<string | null>(
    '',
    [Validators.required, Validators.maxLength(10)],
  );
  protected textMultiLineFormControl = new FormControl<string | null>('');
  protected textMultiLineConstrainedFormControl = new FormControl<
    string | null
  >('', [Validators.required, Validators.maxLength(120)]);
}
