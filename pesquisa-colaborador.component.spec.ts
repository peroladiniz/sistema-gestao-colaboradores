import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PesquisaColaboradorComponent } from './pesquisa-colaborador.component';

describe('PesquisaColaboradorComponent', () => {
  let component: PesquisaColaboradorComponent;
  let fixture: ComponentFixture<PesquisaColaboradorComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [PesquisaColaboradorComponent]
    });
    fixture = TestBed.createComponent(PesquisaColaboradorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
