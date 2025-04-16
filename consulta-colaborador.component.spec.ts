import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ConsultaColaboradorComponent } from './consulta-colaborador.component';

describe('ConsultaColaboradorComponent', () => {
  let component: ConsultaColaboradorComponent;
  let fixture: ComponentFixture<ConsultaColaboradorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [ConsultaColaboradorComponent]
    });
    fixture = TestBed.createComponent(ConsultaColaboradorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
