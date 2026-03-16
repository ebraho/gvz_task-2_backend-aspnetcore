import { Routes } from '@angular/router';

import { CardDemo } from './demo/card-demo/card-demo';
import { NumberInputDemo } from './demo/number-input-demo/number-input-demo';
import { SelectDemo } from './demo/select-demo/select-demo';
import { TextInputDemo } from './demo/text-input-demo/text-input-demo';
import { AddressDemo } from './demo/address-demo/address-demo';

const NAVIGATION_SECTIONS = {
  PARTS: 'Bauteile',
  COMBINED_PARTS: 'Kombinierte Bauteile',
} as const;

export const routes: Routes = [
  {
    path: 'select',
    component: SelectDemo,
    data: { title: 'Auswahl', section: NAVIGATION_SECTIONS.PARTS },
  },
  {
    path: 'card',
    component: CardDemo,
    data: { title: 'Karte', section: NAVIGATION_SECTIONS.PARTS },
  },
  {
    path: 'text-input',
    component: TextInputDemo,
    data: { title: 'Texteingabe', section: NAVIGATION_SECTIONS.PARTS },
  },
  {
    path: 'number-input',
    component: NumberInputDemo,
    data: { title: 'Zahleingabe', section: NAVIGATION_SECTIONS.PARTS },
  },
  {
    path: 'postal-address',
    component: AddressDemo,
    data: { title: 'Postadresse', section: NAVIGATION_SECTIONS.COMBINED_PARTS },
  },
  { path: '**', redirectTo: '/card' },
];
