import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdminvieworphanageComponent } from './adminvieworphanage.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('AdminvieworphanageComponent', () => {
  let component: AdminvieworphanageComponent;
  let fixture: ComponentFixture<AdminvieworphanageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdminvieworphanageComponent ],
      imports: [ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule]

    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdminvieworphanageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  fit('Frontend_should_create_adminvieworphanage_component', () => {
    expect(component).toBeTruthy();
  });

  fit('Frontend_should_contain_orphanages_heading_in_the_adminvieworphanage_component', () => {
    const componentHTML = fixture.debugElement.nativeElement.outerHTML;
    expect(componentHTML).toContain('Orphanages');
  });
});
