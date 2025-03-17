import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateorphanageComponent } from './createorphanage.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('CreateorphanageComponent', () => {
  let component: CreateorphanageComponent;
  let fixture: ComponentFixture<CreateorphanageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ CreateorphanageComponent ],
      imports: [ReactiveFormsModule, RouterTestingModule, HttpClientTestingModule, FormsModule]

    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(CreateorphanageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });
  fit('Frontend_should_create_createorphanage_component', () => {
    expect(component).toBeTruthy();
  });

  fit('Frontend_should_contain_orphanage_creation_form_heading_in_the_createorphanage_component', () => {
    const componentHTML = fixture.debugElement.nativeElement.outerHTML;
    expect(componentHTML).toContain('Orphanage Creation Form');
  });

});
