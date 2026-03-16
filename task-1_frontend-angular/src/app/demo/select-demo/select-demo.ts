import { Component } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';

import { PartDemo } from '../part-demo';
import { Select } from '../../parts/select/select';

@Component({
  selector: 'app-select-demo',
  imports: [PartDemo, Select],
  templateUrl: './select-demo.html',
  styleUrls: ['./select-demo.scss', '../part-demo-common.scss'],
})
export class SelectDemo {
  protected label = 'Auswahl';
  protected description = [
    'Auswahl aus einer Liste von Optionen.',
    'Eine Option hat ein Label und einen Wert. Es kann festgelegt werden, ob die Auswahl zurückgesetzt werden kann.',
  ];
  protected parameters = [
    {
      name: 'Zurücksetzen',
      valueRange: 'Ja oder Nein',
      required: false,
      defaultValue: 'Nein',
    },
    { name: 'Label', valueRange: '<Text>', required: true },
    {
      name: 'Optionen',
      valueRange: 'Sammlung { Label: <Text>, Wert: <Beliebig> }',
      required: true,
    },
  ];

  protected selectFormControl = new FormControl<number | null>(null);
  protected selectConstrainedFormControl = new FormControl<number | null>(
    null,
    [Validators.required],
  );
  protected selectOptions = [
    { label: 'Eins', value: 1 },
    { label: 'Zwei', value: 2 },
    { label: 'Drei', value: 3 },
  ];
}
