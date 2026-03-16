import { Component, OnInit, inject } from '@angular/core';
import { MatIconRegistry } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { NavigationEnd, Router, RouterLink, RouterOutlet } from '@angular/router';

import { routes } from './app.routes';

interface NavigationLink {
  path: string;
  title: string;
}

interface NavigationSection {
  name: string;
  links: NavigationLink[];
}

@Component({
  selector: 'app-root',
  imports: [MatListModule, MatSidenavModule, MatToolbarModule, RouterLink, RouterOutlet],
  templateUrl: './app.html',
  styleUrl: './app.scss',
})
export class App implements OnInit {
  protected activeNavigationLink: string | null = null;
  protected navigationSections: NavigationSection[] = this.buildNavigationSections();

  private readonly matIconRegistry = inject(MatIconRegistry);
  private readonly router = inject(Router);

  ngOnInit(): void {
    this.matIconRegistry.setDefaultFontSetClass('material-symbols-outlined');
    this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.activeNavigationLink = this.router.url.replaceAll('/', '');
      }
    });
  }

  private buildNavigationSections(): NavigationSection[] {
    const sectionMap = new Map<string, NavigationLink[]>();

    routes
      .filter((route) => route.path !== '**')
      .forEach((route) => {
        const section: string = (route.data?.['section'] as string) ?? 'Bauteile';
        const link: NavigationLink = {
          path: route.path!,
          title: route.data?.['title'] as string,
        };
        if (!sectionMap.has(section)) {
          sectionMap.set(section, []);
        }
        sectionMap.get(section)!.push(link);
      });

    return Array.from(sectionMap.entries()).map(([name, links]) => ({ name, links }));
  }
}
